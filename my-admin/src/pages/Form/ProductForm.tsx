import { Field, FieldArray, Form, Formik } from "formik";
import { observer } from "mobx-react-lite";
import React, { useEffect, useState } from "react";
import { Link, useHistory } from "react-router-dom";
import { Button, Header, Segment } from "semantic-ui-react";
import { Brand } from "../../api/models/brand";
import { ProductFormValues } from "../../api/models/product";
import { useStore } from "../../api/store/store";
import MySelectInput from "../../components/form/MySelectInput";
import MyTextArea from "../../components/form/MyTextArea";
import MyTextInput from "../../components/form/MyTextInput";
import LoadingComponent from "../../components/LoadingComponent";
import * as Yup from "yup";

export default observer(function ProductForm() {
  const history = useHistory();
  const { brandStore, productStore, categoryStore } = useStore();
  const { brands, Boption, loadBrands } = brandStore;
  const { categories, Coption, loadCategories } = categoryStore;
  const { createProduct, loadProducts } = productStore;
  const [product, setProduct] = useState<ProductFormValues>(
    new ProductFormValues()
  );
  useEffect(() => {
    if (brands.length == 0 && categories.length == 0) {
      loadBrands();
      loadCategories();
    }
  }, [brands.length, loadBrands, categories.length, loadCategories]);

  const validationSchema = Yup.object({
    name: Yup.string().required("The product name is required!"),
    price: Yup.number()
      .required("The product price is required!")
      .moreThan(0, "should be more than 0"),
    description: Yup.string().required(),
    categoryName: Yup.lazy((val) =>
      Array.isArray(val)
        ? Yup.array().of(
            Yup.string().required("The products category need to be provided!")
          )
        : Yup.string().required("The products category need to be provided!")
    ),
    brandId: Yup.number().required("The product's brand need to be provided"),
  });

  const handleFormSubmit = (product: ProductFormValues) => {
    if (product.id == 0) {
      createProduct(product).then(() => {
        setTimeout(() => {
          history.push(`/productslist`);
        }, 2000);
        loadProducts();
      });
    } else {
      console.log(product);
    }
  };

  if (brands.length == 0 || Boption.length == 0 || Coption.length == 0)
    return <LoadingComponent content="Loading Form..." />;

  return (
    <Segment clearing>
      <Header content="Product details" sub color="teal" />
      <Formik
        enableReinitialize
        initialValues={product}
        onSubmit={(values) => handleFormSubmit(values)}
        validationSchema={validationSchema}
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
              disabled={isSubmitting || !dirty || !isValid}
              loading={isSubmitting}
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
