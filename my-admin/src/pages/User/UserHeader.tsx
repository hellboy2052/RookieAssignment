import { observer } from "mobx-react-lite";
import React from "react";
import { NavLink } from "react-router-dom";
import { Button, Grid, Header } from "semantic-ui-react";

const buttonAdd = {
  position: "absolute",
  right: "1.6%"
}
export default observer(function UserHeader() {
  return (
    <Grid>
      <Grid.Column width={16} textAlign="center">
        <Header as="h1" icon="boxes" content="User List" />
      </Grid.Column>
    </Grid>
  );
});
