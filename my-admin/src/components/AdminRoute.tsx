import React from "react";
import { Redirect, Route, RouteComponentProps, RouteProps } from "react-router";
import { useStore } from "../api/store/store";

interface Props extends RouteProps {
  component:
    | React.ComponentType<RouteComponentProps<any>>
    | React.ComponentType<any>;
}

export default function AdminRoute({ component: Component, ...rest }: Props) {
  const {
    userStore: { isLoggedIn, user },
  } = useStore();
  
  
  return (
    <Route
      {...rest}
      render={(props) =>
        isLoggedIn && user!.roles[0] == "superadmin" ? <Component {...props} /> : <Redirect to="/dashboard" />
      }
    />
  );
}
