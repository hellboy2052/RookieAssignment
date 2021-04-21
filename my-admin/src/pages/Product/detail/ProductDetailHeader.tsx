import { format } from "date-fns";
import { observer } from "mobx-react-lite";
import React from "react";
import { Grid, Icon, Image, Item, Segment } from "semantic-ui-react";
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
export default observer(function ProductDetailHeader({product} : Props) {
  return (
    <Grid.Column width={5}>
      <Image
        fluid
        src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR5RrstOx8dqsxf4Nckc4BUvoFiODJaq8f_oTLZ92DX3Hk_LvPG1l0bFARyvg&usqp=CAc"
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
              {format(product.updatedDate ? product.updatedDate : 0, "dd MMM yyyy")}
            </p>
          </Grid.Column>
        </Grid>
      </Segment>
    </Grid.Column>
  );
});
