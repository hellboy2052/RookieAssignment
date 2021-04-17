import React, { useEffect, useState } from "react";
import { Menu, Sidebar } from "semantic-ui-react";
import SubMenu from "../components/SubMenu";

export default function Navbar() {
  const [menuItem, setmenuItem] = useState([]);

  useEffect(() => {
    fetch("/data/Navbar.json", {
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
      },
    })
      .then((res) => {
        return res.json();
      })
      .then((res) => setmenuItem(res));
  }, []);
  return (
    <>
      <Sidebar
        className="fixed"
        inverted
        as={Menu}
        vertical
        visible={true}
      >
          {menuItem.map(item => (
            <SubMenu key={menuItem.indexOf(item)}  item={item}/>
          ))}
      </Sidebar>
    </>
  );
}
