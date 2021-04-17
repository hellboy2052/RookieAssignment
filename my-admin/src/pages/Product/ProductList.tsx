import React from "react";
import { Segment } from "semantic-ui-react";
import ProductTable from "../../components/ProductTable";

export default function ProductList() {
  return (
    <>
      <Segment>
        <ProductTable />
      </Segment>
    </>
  );
}
