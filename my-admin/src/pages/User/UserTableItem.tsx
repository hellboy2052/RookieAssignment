import { observer } from "mobx-react-lite";
import React from "react";
import { Table } from "semantic-ui-react";
import { UserData } from "../../api/models/user";

interface Props {
  user: UserData;
}
export default observer(function UserTableItem({ user }: Props) {
  return (
    <>
      <Table.Row>
        <Table.Cell>{user.fullname}</Table.Cell>
        <Table.Cell>{user.username}</Table.Cell>
        <Table.Cell>{user.email}</Table.Cell>
        <Table.Cell>{user.roles[0]}</Table.Cell>
      </Table.Row>
    </>
  );
});
