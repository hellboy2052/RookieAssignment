import { observer } from "mobx-react-lite";
import React from "react";
import { Table } from "semantic-ui-react";
import { useStore } from "../../api/store/store";
import ProductTableItem from "./ProductTableItem";

export default observer(function ProductTable() {
  const { productStore } = useStore();
  const { ProductsByDate } = productStore;

  ProductsByDate.forEach((product) => {
    console.log(product.name);
  });

  return (
    <Table celled>
      <Table.Header>
        <Table.Row>
          <Table.HeaderCell>Id</Table.HeaderCell>
          <Table.HeaderCell>Name</Table.HeaderCell>
          <Table.HeaderCell>Price</Table.HeaderCell>
          <Table.HeaderCell>CreateDate</Table.HeaderCell>
          <Table.HeaderCell>UpdateDate</Table.HeaderCell>
          <Table.HeaderCell>Action</Table.HeaderCell>
        </Table.Row>
      </Table.Header>

      <Table.Body>
        {ProductsByDate.map((product) => {
          return <ProductTableItem key={product.id} product={product} />;
        })}
      </Table.Body>

      {/* <Table.Footer>
        <Table.Row>
          <Table.HeaderCell colSpan="3">
            <Menu floated="right" pagination>
              <Menu.Item as="a" icon>
                <Icon name="chevron left" />
              </Menu.Item>
              <Menu.Item as="a">1</Menu.Item>
              <Menu.Item as="a">2</Menu.Item>
              <Menu.Item as="a">3</Menu.Item>
              <Menu.Item as="a">4</Menu.Item>
              <Menu.Item as="a" icon>
                <Icon name="chevron right" />
              </Menu.Item>
            </Menu>
          </Table.HeaderCell>
        </Table.Row>
      </Table.Footer> */}
    </Table>
  );
});
