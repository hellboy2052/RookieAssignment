import React from "react";
import { Message } from "semantic-ui-react";

interface Props {
  errors: any;
}

export default function ValidationErrors({ errors }: Props) {
  return (
    <Message error style={{display: "block"}} >
      {errors && (
        <Message.List>
          {errors.map((err: any, i: any) => {
            return <Message.Item key={i}>{err}</Message.Item>;
          })}
        </Message.List>
      )}
    </Message>
  );
}