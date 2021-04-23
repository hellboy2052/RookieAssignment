import { observer } from "mobx-react-lite";
import React from "react";
import { Button, Header, Menu, Sidebar } from "semantic-ui-react";
import { useStore } from "../api/store/store";
import SubMenu from "../components/SubMenu";

const navItem = [
  {
    name: "Dashboard",
    path: "/dashboard",
  },
  {
    name: "Products",
    path: "/products",
    sub: [
      {
        name: "Product list",
        path: "/productslist",
      },
      {
        name: "Brand list",
        path: "/brandslist",
      },
      {
        name: "Category list",
        path: "/Categories/",
      },
      {
        name: "Add Product",
        path: "/product-form",
      },
    ],
  },
  {
    name: "Users",
    path: "/users",
    sub: [
      {
        name: "User list",
        path: "/usersList",
      },
    ],
  },
];
export default observer(function Navbar() {
  const {
    userStore: { user, logout },
  } = useStore();

  return (
    <>
      <Sidebar
        className="fixed"
        inverted
        as={Menu}
        vertical
        visible={true}
        style={{ width: "16%" }}
      >
        <Header
          as="h2"
          textAlign="center"
          color="green"
          style={{ marginTop: "10px" }}
        >
          Hello {user?.username}
        </Header>
        {navItem.map((item) => {
          if (item.name != "Users") {
            return <SubMenu key={navItem.indexOf(item)} item={item} />;
          }
          if (item.name == "Users" && user?.roles[0] == "superadmin") {
            return <SubMenu key={navItem.indexOf(item)} item={item} />;
          }
        })}
        <Menu.Item>
          <Button
            type="button"
            color="instagram"
            content="Logout"
            fluid
            positive
            onClick={logout}
          />
        </Menu.Item>
      </Sidebar>
    </>
  );
});
