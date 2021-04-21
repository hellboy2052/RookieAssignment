import { useField } from "formik";
import React from "react";
import { Form, Label, Select } from "semantic-ui-react";

interface Props {
  placeholder: string;
  name: string;
  options: any;
  label?: string;
}
export default function MySelectInput(prop: Props) {
  const [field, meta, helpers] = useField(prop.name);

  return (
    <Form.Field error={meta.touched && !!meta.error}>
      <label>{prop.label}</label>
      <Select
        clearable
        options={prop.options}
        value={field.value || null}
        onChange={(e, d) => helpers.setValue(d.value)}
        onBlur={() => helpers.setTouched(true)}
        placeholder={prop.placeholder}
      />
      {meta.touched && meta.error ? (
        <Label basic color="red">
          {meta.error}
        </Label>
      ) : null}
    </Form.Field>
  );
}
