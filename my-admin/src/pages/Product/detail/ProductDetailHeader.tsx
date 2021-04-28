import { format } from "date-fns";
import { observer } from "mobx-react-lite";
import React from "react";
import { NavLink } from "react-router-dom";
import { Button, Grid, Icon, Image, Segment } from "semantic-ui-react";
import { Product } from "../../../api/models/product";

const IconCenterPosition = {
  position: "absolute",
  bottom: "50%",
  left: "50%",
  width: "100%",
  height: "auto",
  transform: "translate(-50%, 50%)",
};

interface Props {
  product: Product;
}
export default observer(function ProductDetailHeader({ product }: Props) {
  return (
    <Grid.Column width={5}>
      <Image
        fluid
        src={product.image || "https://dominionmartialarts.com/wp-content/uploads/2017/04/default-image-620x600.jpg"}
      />

      <Segment attached="top">
        <Grid>
          <Grid.Column width={3}>
            <Icon
              size="big"
              color="teal"
              name="calendar"
              style={IconCenterPosition}
            />
          </Grid.Column>
          <Grid.Column width={13}>
            <p>
              <strong>Created at: </strong>
              {format(product.createdDate!, "dd MMM yyyy")}
            </p>
            <p>
              <strong>Updated at: </strong>
              {format(
                product.updatedDate ? product.updatedDate : 0,
                "dd MMM yyyy"
              )}
            </p>
          </Grid.Column>
        </Grid>
      </Segment>
      <Segment basic>
        <Button as={NavLink} to={"/productslist"} content="Go back" positive />
      </Segment>
    </Grid.Column>
  );
});
