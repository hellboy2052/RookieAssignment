import { observer } from "mobx-react-lite";
import React from "react";
import { Table } from "semantic-ui-react";
import { useStore } from "../../api/store/store";
import UserTableItem from "./UserTableItem";

export default observer(function UserTable() {
  const { userStore } = useStore();
  const { UserByUsername } = userStore;


  UserByUsername.forEach(user => {
      console.log(user.username);
      
  })
  
  return (
    <Table celled>
      <Table.Header>
        <Table.Row>
          <Table.HeaderCell>Fullname</Table.HeaderCell>
          <Table.HeaderCell>Username</Table.HeaderCell>
          <Table.HeaderCell>Email</Table.HeaderCell>
          <Table.HeaderCell>Role</Table.HeaderCell>
        </Table.Row>
      </Table.Header>

      <Table.Body>
        {UserByUsername.map((user) => {
          return <UserTableItem key={user.username} user={user} />;
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
