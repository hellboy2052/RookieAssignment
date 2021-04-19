import { observer } from "mobx-react-lite";
import React from "react";
import { Segment } from "semantic-ui-react";
import ProductTable from "../../components/ProductTable";

export default observer(function ProductList() {
  return (
    <>
      <Segment>
        <ProductTable />
      </Segment>
    </>
  );
});
