import { ErrorMessage, Form, Formik } from "formik";
import { observer } from "mobx-react-lite";
import React from "react";
import { Button, Label, Segment } from "semantic-ui-react";
import { useStore } from "../api/store/store";
import MyTextInput from "./MyTextInput";

export default observer(function LoginForm() {
  const { userStore } = useStore();
  return (
    <Formik
      initialValues={{ email: "", password: "", error: null }}
      onSubmit={(values, { setErrors }) =>
        userStore
          .login(values)
          .catch((error) => setErrors({ error: "Invalid email or password" }))
      }
    >
      {({ handleSubmit, isSubmitting, errors }) => (
        <Form className="ui form" autoComplete="off" onSubmit={handleSubmit}>
          <Segment stacked>
            <MyTextInput name="email" placeholder="E-mail address" />
            <MyTextInput
              name="password"
              placeholder="Password"
              type="password"
            />
            <ErrorMessage
              name="error"
              render={() => {
                return (
                  <Label
                    basic
                    color="red"
                    style={{ marginBottom: 10 }}
                    content={errors.error}
                  />
                );
              }}
            />

            <Button
              loading={isSubmitting}
              type="submit"
              color="teal"
              fluid
              size="large"
              content="Login"
            />
          </Segment>
        </Form>
      )}
    </Formik>
  );
});
