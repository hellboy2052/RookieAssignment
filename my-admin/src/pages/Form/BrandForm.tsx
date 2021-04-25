import { Form, Formik } from "formik";
import { observer } from "mobx-react-lite";
import React, { useEffect, useState } from "react";
import { Link, useHistory, useParams } from "react-router-dom";
import { Button, Header, Segment } from "semantic-ui-react";
import { BrandFormValues } from "../../api/models/brand";
import { useStore } from "../../api/store/store";
import MyTextInput from "../../components/form/MyTextInput";
import LoadingComponent from "../../components/LoadingComponent";
import * as Yup from "yup";

export default observer(function BrandForm() {
  const history = useHistory();
  const { brandStore } = useStore();
  const {
    createBrand,
    loadBrand,
    loadingInitial,
    setLoadingInitial,
    clearBrands,
  } = brandStore;
  const [brand, setBrand] = useState<BrandFormValues>(new BrandFormValues());

  const { id } = useParams<{ id: string }>();

  useEffect(() => {
    if (id) {
      loadBrand(id).then((brand) => {
        setBrand(
          new BrandFormValues({
            id: brand!.id,
            name: brand!.name,
          })
        );
      });
    } else {
      setLoadingInitial(false);
    }
  }, [loadBrand, id, setLoadingInitial]);

  const validationSchema = Yup.object({
    name: Yup.string().required("The brand name is required!"),
  });

  const handleFormSubmit = (brand: BrandFormValues) => {
    if (brand.id === 0) {
      createBrand(brand).then(() => {
        setTimeout(() => {
          history.push(`/brandslist`);
        }, 2000);
        clearBrands();
      });
    } else {
      // updateProduct(product).then(() => {
      //   setTimeout(() => {
      //     history.push(`/products/${product.id}`);
      //   }, 2000);
      // });
    }
  };

  if (loadingInitial) return <LoadingComponent content="Loading Form..." />;

  return (
    <Segment clearing>
      <Header content="Brand details" sub color="teal" />
      <Formik
        enableReinitialize
        initialValues={brand}
        onSubmit={(values) => handleFormSubmit(values)}
        validationSchema={validationSchema}
      >
        {({ handleSubmit, isValid, isSubmitting, dirty, values }) => (
          <Form className="ui form" onSubmit={handleSubmit} autoComplete="off">
            <MyTextInput name="name" placeholder="Name" />

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
              to="/brandslist"
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
