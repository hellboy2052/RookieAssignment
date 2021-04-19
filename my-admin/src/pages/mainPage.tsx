import { ErrorMessage, Form, Formik } from "formik";
import { observer } from "mobx-react-lite";
import React from "react";
import { Redirect } from "react-router";
import { Button, Grid, Header, Label, Segment } from "semantic-ui-react";
import { useStore as UseStore } from "../api/store/store";
import LoginForm from "../components/LoginForm";
import MyTextInput from "../components/MyTextInput";

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
