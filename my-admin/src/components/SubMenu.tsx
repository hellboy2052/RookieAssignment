import React, { useState } from "react";
import { NavLink } from "react-router-dom";
import { Menu } from "semantic-ui-react";

const style = {
  textAlign: "left",
}

interface Props {
  item: {
    name: string;
    path: string;
    sub: [{ name: string; path: string }] | null;
  };
}
export default function SubMenu({ item }: Props) {
  const [state,setstate] = useState(false);

  const openSub = () => setstate(!state);
  
  return (
    <>
      <Menu.Item key={item.name} className={state && item.sub ? "active" : ""} as={!item.sub ? NavLink : Menu.Item} to={item.path} style={style} onClick={openSub}>
        {item.name}
      </Menu.Item>
      {item.sub && state &&
        item.sub.map((i) => (
          <Menu.Item className="menuSub" key={i.name} as={NavLink} to={i!.path} style={style}>
            {i!.name}
          </Menu.Item>
        ))}
    </>
  );
}
