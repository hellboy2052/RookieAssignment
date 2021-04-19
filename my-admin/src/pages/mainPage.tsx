import { observer } from "mobx-react-lite";
import React from "react";
import { Redirect } from "react-router";
import { Grid, Header } from "semantic-ui-react";
import { useStore as UseStore } from "../api/store/store";
import LoginForm from "../components/LoginForm";

export default observer(function mainPage() {
  const { userStore } = UseStore();

  if (userStore.isLoggedIn) return <Redirect to="/dashboard" />;
  return (
    <Grid textAlign="center" style={{ height: "100vh" }} verticalAlign="middle">
      <Grid.Column style={{ maxWidth: 450 }}>
        <Header as="h2" color="teal" textAlign="center">
          Login to Admin
        </Header>
        <LoginForm />
      </Grid.Column>
    </Grid>
  );
});
