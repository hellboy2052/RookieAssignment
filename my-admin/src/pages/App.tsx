import React from "react";
import { Route } from "react-router";
import { Container, Grid } from "semantic-ui-react";
import mainPage from "./mainPage";
import Navbar from "./Navbar";
import ProductList from "./Product/ProductList";

function App() {
  return (
    <>
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
                  <Route path="/products/list" component={ProductList} />
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

export default App;
