import { useField } from "formik";
import React from "react";
import { Form, Label } from "semantic-ui-react";

interface Props {
  placeholder: string;
  name: string;
  rows: number;
  label?: string;
}
export default function MyTextArea(prop: Props) {
  const [field, meta] = useField(prop.name);

  return (
    <Form.Field error={meta.touched && !!meta.error}>
      <label>{prop.label}</label>
      <textarea {...field} {...prop} />
      {meta.touched && meta.error ? (
        <Label basic color="red">
          {meta.error}
        </Label>
      ) : null}
    </Form.Field>
  );
}
