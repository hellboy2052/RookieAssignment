import { Field, FieldArray, Form, Formik } from "formik";
import { observer } from "mobx-react-lite";
import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { Button, Header, Segment } from "semantic-ui-react";
import { Brand } from "../../api/models/brand";
import { ProductFormValues } from "../../api/models/product";
import { useStore } from "../../api/store/store";
import MySelectInput from "../../components/form/MySelectInput";
import MyTextArea from "../../components/form/MyTextArea";
import MyTextInput from "../../components/form/MyTextInput";
import LoadingComponent from "../../components/LoadingComponent";

const categoryOption = [
  { text: "Laptop", value: "Laptop" },
  { text: "Shoes", value: "Shoe" },
];

const brandOption = [
  { text: "Asus", value: 1 },
  { text: "Dell", value: 2 },
];

export default observer(function ProductForm() {
  const { brandStore, productStore, categoryStore } = useStore();
  const { brands, Boption, loadBrands } = brandStore;
  const { categories, Coption, loadCategories } = categoryStore;
  const [product, setProduct] = useState<ProductFormValues>(
    new ProductFormValues()
  );
  useEffect(() => {
    if (brands.length == 0 && categories.length == 0) {
      loadBrands();
      loadCategories();
    }
  }, [brands.length, loadBrands, categories.length, loadCategories]);
  if (brands.length == 0 || Boption.length == 0 || Coption.length == 0)
    return <LoadingComponent content="Loading Form..." />;

  return (
    <Segment clearing>
      <Header content="Product details" sub color="teal" />
      <Formik
        enableReinitialize
        initialValues={product}
        onSubmit={(values) => console.log(values)}
      >
        {({ handleSubmit, isValid, isSubmitting, dirty, values }) => (
          <Form className="ui form" onSubmit={handleSubmit} autoComplete="off">
            <MyTextInput name="name" placeholder="Name" />
            <MyTextInput type="number" name="price" placeholder="Price" />

            <MyTextArea rows={3} placeholder="Description" name="description" />

            <FieldArray
              name="categoryName"
              render={(arrayHelpers) => (
                <div className="field">
                  {values.categoryName && values.categoryName.length > 0 ? (
                    values.categoryName.map((category, index) => (
                      <div key={index}>
                        <Field
                          component={() => (
                            <MySelectInput
                              options={Coption}
                              placeholder="Category"
                              label="Category"
                              name={`categoryName.${index}`}
                            />
                          )}
                        />
                      </div>
                    ))
                  ) : (
                    <Button type="button" onClick={() => arrayHelpers.push("")}>
                      {/* show this when user has removed all Categories from the list */}
                      Add a Category
                    </Button>
                  )}
                </div>
              )}
            />
            <MySelectInput
              options={Boption}
              placeholder="Brand"
              label="brand"
              name="brandId"
            />

            <Button
              //   disabled={isSubmitting || !dirty || !isValid}
              //   loading={isSubmitting}
              floated="right"
              positive
              type="submit"
              content="Submit"
            />
            <Button
              as={Link}
              to="/productslist"
              floated="right"
              type="button"
              content="Cancel"
            />
          </Form>
        )}
      </Formik>
    </Segment>
  );
});
