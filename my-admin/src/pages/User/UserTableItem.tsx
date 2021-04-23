import { format } from "date-fns";
import { observer } from "mobx-react-lite";
import React, { SyntheticEvent, useState } from "react";
import { NavLink } from "react-router-dom";
import { Button, Table } from "semantic-ui-react";
import { UserData } from "../../api/models/user";
import { useStore } from "../../api/store/store";

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
