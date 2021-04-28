import { observer } from "mobx-react-lite";
import React, { useState } from "react";
import {
  Button,
  Grid,
  Label,
  Segment,
  Image,
  Divider,
} from "semantic-ui-react";
import { Product } from "../../../api/models/product";

const textarea = {
  background: "#eee",
  overflow: "hidden",
  textOverflow: "ellipsis",
  width: "100%",
};
interface Props {
  product: Product;
}
export default observer(function ProductDetailInfo({ product }: Props) {
  const [readToggle, setreadToggle] = useState(false);

  const handlerToggle = () => setreadToggle(!readToggle);
  return (
    <Grid.Column width={11}>
      <Segment.Group>
        <Segment attached="top">
          <Grid>
            <Grid.Column width={11}>
              <strong>Name: </strong>
              {product.name}
            </Grid.Column>
            <Grid.Column width={5}>
              <strong>Price: </strong>
              {product.price}
            </Grid.Column>
          </Grid>
          <Grid verticalAlign="middle">
            <Grid.Column width={3}>
              <strong>Brand: </strong>
              <span>{product.brandName}</span>
            </Grid.Column>
            <Grid.Column width={13}>
              <strong>Category: </strong>
              {product.productCategories.map((category) => (
                <Label color="teal" horizontal key={category.id}>
                  {category.name}
                </Label>
              ))}
            </Grid.Column>
          </Grid>
          <Grid verticalAlign="middle">
            <Grid.Column width="16">
              <strong>Rate point: </strong>
              <span>{product.rating}</span>pt
            </Grid.Column>
          </Grid>
        </Segment>
        <Segment>
          <Grid>
            <Grid.Column width={16}>
              <strong>Description:</strong>
            </Grid.Column>
          </Grid>
          <Grid>
            <Grid.Column width={16}>
              <div
                style={!readToggle ? { ...textarea, whiteSpace: "nowrap" } : {}}
              >
                <span>{product.description}</span>
              </div>
            </Grid.Column>
          </Grid>
          <Grid>
            <Grid.Column width={16}>
              <Button
                size="mini"
                color="grey"
                content={!readToggle ? "Read more" : "Read less"}
                onClick={handlerToggle}
              />
            </Grid.Column>
          </Grid>
        </Segment>

        {product.pictures!.length > 0 && (
          <>
            <Divider hidden />
            <Segment>
              <Image.Group size="small">
                {product.pictures &&
                  product.pictures.map((p, i) => <Image src={p.url} key={i} />)}
              </Image.Group>
            </Segment>
          </>
        )}
      </Segment.Group>
    </Grid.Column>
  );
});
