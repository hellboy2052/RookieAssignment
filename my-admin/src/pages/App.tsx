import { observer } from "mobx-react-lite";
import React, { useEffect } from "react";
import { Redirect, Route } from "react-router";
import { ToastContainer } from "react-toastify";
import { Container, Grid } from "semantic-ui-react";
import { useStore } from "../api/store/store";
import LoadingComponent from "../components/LoadingComponent";
import PrivateRoute from "../components/PrivateRoute";
import DashBoard from "./dashboard/DashBoard";
import mainPage from "./mainPage";
import Navbar from "./Navbar";
import ProductDetail from "./Product/ProductDetail";
import ProductList from "./Product/ProductList";

function App() {
  const { userStore, commonStore } = useStore();

  useEffect(() => {
    if (commonStore.token) {
      userStore.getUser().finally(() => commonStore.setAppLoaded());
    } else {
      commonStore.setAppLoaded();
    }
  }, [commonStore, userStore]);

  if(!commonStore.appLoaded) return <LoadingComponent />

  return (
    <>
      <ToastContainer position="bottom-right" hideProgressBar/>
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
                <Grid.Column width={13}>
                  <PrivateRoute path="/dashboard" component={DashBoard} />
                  <PrivateRoute exact path="/products" component={() => <Redirect to="/productslist" />} />
                  <PrivateRoute path="/productslist/" component={ProductList} />
                  <PrivateRoute path="/products/:id" component={ProductDetail} />
                  <footer className="sticky-footer bg-white">
                    <div className="container my-auto">
                      <div className="copyright text-center my-auto">
                        <span>Copyright © Your Website 2020</span>
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
