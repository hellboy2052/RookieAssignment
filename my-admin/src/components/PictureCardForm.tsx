import { observer } from "mobx-react-lite";
import React, { SyntheticEvent, useEffect, useState } from "react";
import { Card, Button, Image } from "semantic-ui-react";
import { history } from "..";
import { Picture, Product } from "../api/models/product";
import { useStore } from "../api/store/store";

interface Props {
  product: Product;
  setPicture: (pro : Product) => void;
}
export default observer(function PictureCardForm({ product, setPicture }: Props) {
  const [target, setTarget] = useState("");
  const [targetMain, setTargetMain] = useState("");
  const {
    productStore: { setMainPicture, loading, deletePicture },
  } = useStore();

  useEffect(() => {
    if (targetMain === "") {
      setTargetMain("main" + product!.pictures!.find((p) => p.isMain)!.id);
    }
  }, []);
  function handleSetMainPhoto(
    picture: Picture,
    id: string,
    e: SyntheticEvent<HTMLButtonElement>
  ) {
    setTarget(e.currentTarget.name);
    setTargetMain(e.currentTarget.name);
    setMainPicture(picture, id);
  }

  function handleDeletePhoto(
    picture: Picture,
    id: string,
    e: SyntheticEvent<HTMLButtonElement>
  ) {
    setTarget(e.currentTarget.name);
    deletePicture(picture, id).then(() => {
      var p = {...product, pictures: product!.pictures!.filter(p => p.id !== picture.id) }
      setPicture(p);
    });
    
  }

  return (
    <>
      {product.pictures!.map((p, i) => (
        <Card key={p.id}>
          <Image src={p.url} />
          <Button.Group>
            <Button
              basic
              color="green"
              content="Main"
              name={"main" + p.id}
              disabled={targetMain === "main" + p.id}
              loading={target === "main" + p.id && loading}
              onClick={(e) => handleSetMainPhoto(p, product.id.toString() , e)}
            />
            <Button
              basic
              color="red"
              icon="trash"
              loading={target === p.id && loading}
              disabled={targetMain === "main" + p.id}
              onClick={(e) => handleDeletePhoto(p, product.id.toString(), e)}
              name={p.id}
            />
          </Button.Group>
        </Card>
      ))}
    </>
  );
});
