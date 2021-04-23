import { observer } from "mobx-react-lite";
import React, { useEffect } from "react";
import { Segment } from "semantic-ui-react";
import { useStore } from "../../api/store/store";
import LoadingComponent from "../../components/LoadingComponent";
import UserHeader from "./UserHeader";
import UserTable from "./UserTable";

export default observer(function UserList() {
  const { userStore } = useStore();
  const { loadUser, userRegistry } = userStore;

  useEffect(() => {
    if (userRegistry.size <= 1) loadUser();
  }, [userRegistry.size, loadUser]);

  if (userStore.loadingInitial)
    return <LoadingComponent content="Loading Users..." />;

  return (
    <>
      <Segment>
        <UserHeader />
        <UserTable />
      </Segment>
    </>
  );
});
