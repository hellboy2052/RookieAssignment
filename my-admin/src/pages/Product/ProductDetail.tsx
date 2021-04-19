import { observer } from "mobx-react-lite";
import React, { useEffect } from "react";
import { useParams } from "react-router";
import { useStore } from "../../api/store/store";
import LoadingComponent from "../../components/LoadingComponent";

export default observer(function ProductDetail() {
  const { productStore } = useStore();
  const {
    selectedProduct: product,
    loadProduct,
    loadingInitial,
    clearSelectedProduct,
  } = productStore;

  const { id } = useParams<{ id: string }>();

  useEffect(() => {
    if (id) {
      loadProduct(id);
    }
    return () => clearSelectedProduct();
  }, [id, loadProduct, clearSelectedProduct]);

  if (loadingInitial || !product)
    return <LoadingComponent content="Loading a Product..." />;
  console.log(product);
  return (
    <>
      <p>Hello product {id}</p>
    </>
  );
});
