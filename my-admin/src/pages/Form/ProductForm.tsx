import { Field, FieldArray, Form, Formik } from "formik";
import { observer } from "mobx-react-lite";
import React, { useEffect, useState } from "react";
import { Link, useHistory, useParams } from "react-router-dom";
import {
  Button,
  Divider,
  Header,
  Segment,
  Image,
  Card,
} from "semantic-ui-react";
import { Picture, Product, ProductFormValues } from "../../api/models/product";
import { useStore } from "../../api/store/store";
import MySelectInput from "../../components/form/MySelectInput";
import MyTextArea from "../../components/form/MyTextArea";
import MyTextInput from "../../components/form/MyTextInput";
import LoadingComponent from "../../components/LoadingComponent";
import * as Yup from "yup";
import PictureCardForm from "../../components/PictureCardForm";

const dropzoneStyle = {
  width: "100%",
  height: "auto",
  borderWidth: 2,
  borderColor: "rgb(102, 102, 102)",
  borderStyle: "dashed",
  borderRadius: 5,
};

export default observer(function ProductForm() {
  const history = useHistory();
  const { brandStore, productStore, categoryStore } = useStore();
  const { brands, Boption } = brandStore;
  const { Coption } = categoryStore;
  const {
    updateProduct,
    createProduct,
    loadProducts,
    loadProduct,
    loadingInitial,
    setLoadingInitial,
  } = productStore;
  const [product, setProduct] = useState<ProductFormValues>(
    new ProductFormValues()
  );

  const [loadingData, SetLoadingData] = useState(true);
  const [productselect, setProductselect] = useState<Product | null>(null);

  const { id } = useParams<{ id: string }>();

  
  useEffect(() => {
    if (id) {
      loadProduct(id).then((product) => {
        setTimeout(() => {
          setProduct(
            new ProductFormValues({
              id: product!.id,
              name: product!.name,
              price: product!.price,
              description: product!.description,
              image: product!.image,
              brandId: brands.find((x) => x.name === product!.brandName)!.id,
              categoryName: product!.productCategories.map((x) => {
                return x.name;
              }),
            })
          );
          setProductselect(product || null);
          SetLoadingData(false);
        }, 1000);
      });
    } else {
      setLoadingInitial(false);
    }
  }, [loadProduct, id, setLoadingInitial, brands]);

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
    if (product.id === 0) {
      createProduct(product).then(() => {
        setTimeout(() => {
          history.push(`/productslist`);
        }, 2000);
        loadProducts();
      });
    } else {
      updateProduct(product).then(() => {
        setTimeout(() => {
          history.push(`/products/${product.id}`);
        }, 2000);
      });
    }
  };

  const handleSelectProduct = (product: Product) => {
    setProductselect(product);
  }

  if (
    loadingInitial ||
    brands.length === 0 ||
    Boption.length === 0 ||
    Coption.length === 0
  )
    return <LoadingComponent content="Loading Form..." />;

  if (id && loadingData)
    return <LoadingComponent content="Loading Product Data..." />;

  return (
    <Segment.Group>
      <Segment clearing>
        <Header content="Product details" sub color="teal" />
        <Formik
          enableReinitialize
          initialValues={product}
          onSubmit={(values) => handleFormSubmit(values)}
          validationSchema={validationSchema}
        >
          {({
            handleSubmit,
            isValid,
            isSubmitting,
            dirty,
            values,
            setFieldValue,
          }) => (
            <Form
              className="ui form"
              onSubmit={handleSubmit}
              autoComplete="off"
            >
              <MyTextInput name="name" placeholder="Name" />
              <MyTextInput type="number" name="price" placeholder="Price" />

              <MyTextArea
                rows={3}
                placeholder="Description"
                name="description"
              />

              <FieldArray
                name="categoryName"
                render={(arrayHelpers) => (
                  <div className="field">
                    {values.categoryName.length <= 0 && arrayHelpers.push("")}
                    {values.categoryName &&
                      values.categoryName.length > 0 &&
                      values.categoryName.map((category, index) => (
                        <div key={index}>
                          <Field
                            component={() => (
                              <MySelectInput
                                disable={id ? true : false}
                                options={Coption}
                                placeholder="Category"
                                label="Category"
                                name={`categoryName.${index}`}
                              />
                            )}
                          />
                        </div>
                      ))}
                    {/* <Button type="button" onClick={() => arrayHelpers.push("")}>
                      Add a Category
                    </Button> */}
                  </div>
                )}
              />
              <MySelectInput
                options={Boption}
                placeholder="Brand"
                label="brand"
                name="brandId"
              />

              {/* Image */}
              <div className="field">
                <input
                  type="file"
                  id="file"
                  multiple
                  name="file"
                  onChange={(event) => {
                    const files = event.target.files;
                    if (files) {
                      for (let i = 0; i < files.length; i++) {
                        console.log(files[i].type);
                        if (
                          files[i].type === "image/png" ||
                          files[i].type === "image/jpeg" ||
                          files[i].type === "image/jpg"
                        )
                          setFieldValue(`pictures[${i}]`, files[i]);
                      }
                    }
                  }}
                />
              </div>
              <div className="field">
                {values.pictures &&
                  values.pictures.length > 0 &&
                  values.pictures.map((pic, i) => (
                    <img
                      key={i}
                      style={{ height: "10%", width: "10%" }}
                      src={URL.createObjectURL(pic)}
                    />
                  ))}
              </div>
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
        {/* Gallery image */}
      </Segment>
      {id && productselect && productselect!.pictures!.length > 0 && (
        <>
          <Divider hidden />
          <Segment>
            <Card.Group itemsPerRow={6}>
              <PictureCardForm product={productselect} setPicture={handleSelectProduct}/>
            </Card.Group>
          </Segment>
        </>
      )}
    </Segment.Group>
  );
});
