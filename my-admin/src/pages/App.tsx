import { observer } from "mobx-react-lite";
import React, { useEffect } from "react";
import { Redirect, Route, useLocation } from "react-router";
import { Switch } from "react-router-dom";
import { ToastContainer } from "react-toastify";
import { Container, Grid } from "semantic-ui-react";
import { useStore } from "../api/store/store";
import AdminRoute from "../components/AdminRoute";
import LoadingComponent from "../components/LoadingComponent";
import PrivateRoute from "../components/PrivateRoute";
import BrandList from "./Brand/BrandList";
import DashBoard from "./dashboard/DashBoard";
import NotFound from "./Error/NotFound";
import ServerError from "./Error/ServerError";
import TestErrors from "./Error/TestError";
import BrandForm from "./Form/BrandForm";
import ProductForm from "./Form/ProductForm";
import mainPage from "./mainPage";
import Navbar from "./Navbar";
import ProductDetail from "./Product/detail/ProductDetail";
import ProductList from "./Product/ProductList";
import UserList from "./User/UserList";

function App() {
  const location = useLocation();
  const {
    userStore,
    commonStore: { token, setAppLoaded, appLoaded },
    brandStore,
    categoryStore,
    productStore
  } = useStore();
  const { getUser } = userStore;
  const { loadBrands, brands } = brandStore;
  const { loadCategories, categories } = categoryStore;
  const {productRegistry, loadProducts} = productStore;

  useEffect(() => {
    if (productRegistry.size <= 1) loadProducts();
  }, [productRegistry.size, loadProducts]);

  useEffect(() => {
    if (brands.length === 0) loadBrands();
  }, [loadBrands, brands.length, brands]);

  useEffect(() => {
    if (categories.length === 0) loadCategories();
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
                  <Switch>
                    <PrivateRoute path="/dashboard" component={DashBoard} />
                    <PrivateRoute
                      path="/productslist"
                      component={ProductList}
                    />
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
                        location.key
                          ? location.key.concat("brand")
                          : location.key
                      }
                      path={["/brand-form", "/edit-brand/:id"]}
                      component={BrandForm}
                    />
                    <PrivateRoute path="/errors" component={TestErrors} />
                    <AdminRoute path="/usersList" component={UserList} />
                    <Route path="/server-error" component={ServerError} />
                    <Route component={NotFound} />
                  </Switch>
                  <footer
                    className="sticky-footer bg-white"
                    style={{ marginTop: "20px" }}
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
