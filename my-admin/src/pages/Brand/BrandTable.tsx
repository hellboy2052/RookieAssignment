import { observer } from "mobx-react-lite";
import React from "react";
import { Table } from "semantic-ui-react";
import { useStore } from "../../api/store/store";
import BrandTableItem from "./BrandTableItem";

export default observer(function BrandTable() {
  const { brandStore } = useStore();
  const { brands } = brandStore;


  return (
    <Table celled>
      <Table.Header>
        <Table.Row>
          <Table.HeaderCell>Id</Table.HeaderCell>
          <Table.HeaderCell>Name</Table.HeaderCell>
          <Table.HeaderCell>Action</Table.HeaderCell>
        </Table.Row>
      </Table.Header>

      <Table.Body>
        {brands.map((brand) => {
          return <BrandTableItem key={brand.id} brand={brand} />;
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
