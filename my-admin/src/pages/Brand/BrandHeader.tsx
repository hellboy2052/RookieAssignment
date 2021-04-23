import { observer } from "mobx-react-lite";
import React from "react";
import { NavLink } from "react-router-dom";
import { Button, Grid, Header } from "semantic-ui-react";

const buttonAdd = {
  position: "absolute",
  right: "1.6%"
}
export default observer(function BrandHeader() {
  return (
    <Grid>
      <Grid.Column width={6} textAlign="center">
        <Header as="h1" icon="tags" content="Brand List" />
      </Grid.Column>
      <Grid.Column width={10}>
        <Button as={NavLink} to={"/brand-form"} color="linkedin" content="Add brand" style={buttonAdd}/>
      </Grid.Column>
    </Grid>
  );
});
