import { observer } from "mobx-react-lite";
import React, { useEffect } from "react";
import { Segment } from "semantic-ui-react";
import { useStore } from "../../api/store/store";
import LoadingComponent from "../../components/LoadingComponent";
import ProductHeader from "./ProductHeader";
import ProductTable from "./ProductTable";

export default observer(function ProductList() {
  const { productStore } = useStore();
  const { loadProducts, productRegistry } = productStore;

  useEffect(() => {
    if (productRegistry.size <= 1) loadProducts();
  }, [productRegistry.size, loadProducts]);

  if (productStore.loadingInitial)
    return <LoadingComponent content="Loading Products..." />;

  return (
    <>
      <Segment>
        <ProductHeader />
        <ProductTable />
      </Segment>
    </>
  );
});
