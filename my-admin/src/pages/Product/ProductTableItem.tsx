import { format } from "date-fns";
import { observer } from "mobx-react-lite";
import React from "react";
import { NavLink } from "react-router-dom";
import { Button, Table } from "semantic-ui-react";
import { Product } from "../../api/models/product";

interface Props {
  product: Product;
}
export default observer(function ProductTableItem({ product }: Props) {
  return (
    <>
      <Table.Row>
        <Table.Cell>{product.id}</Table.Cell>
        <Table.Cell>{product.name}</Table.Cell>
        <Table.Cell>{product.price}</Table.Cell>
        <Table.Cell>{format(product.createdDate!, "dd MMM yyyy h:mm aa")}</Table.Cell>
        <Table.Cell>{format(product.updatedDate!, "dd MMM yyyy h:mm aa")}</Table.Cell>
        <Table.Cell width="3">
          <Button as={NavLink} to={`/products/${product.id}`} positive icon="eye" />
          <Button as={NavLink} to={`/edit-product/${product.id}`} icon="pencil" color="teal" />
          <Button icon="trash" color="red" />
        </Table.Cell>
      </Table.Row>
    </>
  );
})
