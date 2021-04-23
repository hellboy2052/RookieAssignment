import { observer } from "mobx-react-lite";
import React, { useEffect } from "react";
import { Redirect, Route, useLocation } from "react-router";
import { ToastContainer } from "react-toastify";
import { Container, Grid } from "semantic-ui-react";
import { useStore } from "../api/store/store";
import LoadingComponent from "../components/LoadingComponent";
import PrivateRoute from "../components/PrivateRoute";
import BrandList from "./Brand/BrandList";
import DashBoard from "./dashboard/DashBoard";
import BrandForm from "./Form/BrandForm";
import ProductForm from "./Form/ProductForm";
import mainPage from "./mainPage";
import Navbar from "./Navbar";
import ProductDetail from "./Product/detail/ProductDetail";
import ProductList from "./Product/ProductList";

function App() {
  const location = useLocation();
  const {
    userStore,
    commonStore: { token, setAppLoaded, appLoaded },
    brandStore,
    categoryStore,
  } = useStore();
  const { getUser } = userStore;
  const { loadBrands, brands } = brandStore;
  const { loadCategories, categories } = categoryStore;

  useEffect(() => {
    if (brands.length == 0) loadBrands();
  }, [loadBrands, brands.length, brands]);

  useEffect(() => {
    if (categories.length == 0) loadCategories();
  }, [loadCategories, categories.length, categories]);
  useEffect(() => {
    if (token) {
      getUser().finally(() => setAppLoaded());
    } else {
      setAppLoaded();
    }
  }, [token, getUser]);

  if (!appLoaded) return <LoadingComponent />;

  return (
    <>
      <ToastContainer position="bottom-right" hideProgressBar />
      <Route exact path="/" component={mainPage} />
      <Route
        path="/(.+)"
        render={() => (
          <>
            <Container fluid>
              <Grid stretched={true}>
                <Grid.Column width={3}>
                  <Navbar />
                </Grid.Column>
                <Grid.Column width={13} style={{ marginTop: "50px" }}>
                  <PrivateRoute path="/dashboard" component={DashBoard} />
                  <PrivateRoute path="/productslist" component={ProductList} />
                  <PrivateRoute path="/brandslist" component={BrandList} />
                  <PrivateRoute
                    exact
                    path="/products"
                    component={() => <Redirect to="/productslist" />}
                  />
                  <PrivateRoute
                    path="/products/:id"
                    component={ProductDetail}
                  />
                  <PrivateRoute
                    key={
                      location.key
                        ? location.key.concat("product")
                        : location.key
                    }
                    path={["/product-form", "/edit-product/:id"]}
                    component={ProductForm}
                  />
                  <PrivateRoute
                    key={
                      location.key ? location.key.concat("brand") : location.key
                    }
                    path={["/brand-form", "/edit-brand/:id"]}
                    component={BrandForm}
                  />
                  <footer
                    className="sticky-footer bg-white"
                    style={{ marginTop: "20px", height: "5%" }}
                  >
                    <div className="container my-auto">
                      <div className="copyright text-center my-auto">
                        <span>Copyright © Your Website 2021</span>
                      </div>
                    </div>
                  </footer>
                </Grid.Column>
              </Grid>
            </Container>
          </>
        )}
      />
    </>
  );
}

export default observer(App);
