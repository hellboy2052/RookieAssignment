import { observer } from "mobx-react-lite";
import React from "react";
import { Grid, Header } from "semantic-ui-react";


export default observer(function UserHeader() {
  return (
    <Grid>
      <Grid.Column width={16} textAlign="center">
        <Header as="h1" icon="boxes" content="User List" />
      </Grid.Column>
    </Grid>
  );
});
