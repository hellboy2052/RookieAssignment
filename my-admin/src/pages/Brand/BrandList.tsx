import { observer } from "mobx-react-lite";
import React, { useEffect } from "react";
import { Segment } from "semantic-ui-react";
import { useStore } from "../../api/store/store";
import LoadingComponent from "../../components/LoadingComponent";
import BrandHeader from "./BrandHeader";
import BrandTable from "./BrandTable";

export default observer(function BrandList() {
  const { brandStore } = useStore();
  const { brands, loadBrands } = brandStore;
  


  if (brandStore.loadingInitial && brands.length == 0)
    return <LoadingComponent content="Loading Brands..." />;

  return (
    <>
      <Segment>
        <BrandHeader />
        <BrandTable />
      </Segment>
    </>
  );
});
