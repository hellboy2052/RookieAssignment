import { observer } from "mobx-react-lite";
import React, { useEffect, useState } from "react";
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
        path: "/products/list",
      },
      {
        name: "Brand list",
        path: "/products/Brands",
      },
      {
        name: "Category list",
        path: "/products/Categories",
      },
      {
        name: "Add Product",
        path: "/products/add-product",
      },
    ],
  },
  {
    name: "Users",
    path: "/users",
    sub: [
      {
        name: "User list",
        path: "/users/list",
      },
      {
        name: "Add User",
        path: "/users/add-user",
      },
    ],
  },
  {
    name: "Logout",
    path: "/logout",
  },
];
export default observer(function Navbar() {
  const [menuItem, setmenuItem] = useState(navItem);

  const {
    userStore: { user, logout },
  } = useStore();

  return (
    <>
      <Sidebar className="fixed" inverted as={Menu} vertical visible={true}>
        <Header
          as="h2"
          textAlign="center"
          color="green"
          style={{ marginTop: "10px" }}
        >
          Hello {user?.username}
        </Header>
        {menuItem.map((item) => (
          <SubMenu key={menuItem.indexOf(item)} item={item} />
        ))}
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
