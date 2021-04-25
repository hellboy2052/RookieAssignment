import { observer } from "mobx-react-lite";
import React, { SyntheticEvent, useState } from "react";
import { NavLink } from "react-router-dom";
import { Button, Table } from "semantic-ui-react";
import { Brand } from "../../api/models/brand";
import { useStore } from "../../api/store/store";

interface Props {
  brand: Brand;
}
export default observer(function BrandTableItem({ brand }: Props) {
  const {
    brandStore: { deleteBrand, loading },
    productStore
  } = useStore();
  const [target, setTarget] = useState("");

  function handleDelete(id: string, e: SyntheticEvent<HTMLButtonElement>) {
    setTarget(e.currentTarget.name);
    deleteBrand(id);
    
    productStore.clearProducts()
  }
  return (
    <>
      <Table.Row>
        <Table.Cell>{brand.id}</Table.Cell>
        <Table.Cell>{brand.name}</Table.Cell>
        <Table.Cell width="3">
          <Button
            as={NavLink}
            to={`/edit-brand/${brand.id}`}
            icon="pencil"
            color="teal"
          />
          <Button
            name={brand.id}
            loading={loading && target === brand.id.toString()}
            icon="trash"
            color="red"
            onClick={(e) => handleDelete(brand.id.toString(), e)}
          />
        </Table.Cell>
      </Table.Row>
    </>
  );
});
