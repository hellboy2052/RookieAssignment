import { observer } from "mobx-react-lite";
import React from "react";
import { NavLink } from "react-router-dom";
import { Button, Grid, Header } from "semantic-ui-react";

const buttonAdd = {
  position: "absolute",
  right: "1.6%"
}
export default observer(function ProductHeader() {
  return (
    <Grid>
      <Grid.Column width={6} textAlign="center">
        <Header as="h1" icon="boxes" content="Product List" />
      </Grid.Column>
      <Grid.Column width={10}>
        <Button as={NavLink} to={"/product-form"} color="linkedin" content="Add product" style={buttonAdd}/>
      </Grid.Column>
    </Grid>
  );
});
