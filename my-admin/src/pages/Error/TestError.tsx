import React, { useState } from "react";
import { Button, Header, Segment } from "semantic-ui-react";
import axios from "axios";
import ValidationErrors from "./ValidationErrors";

export default function TestErrors() {
  const baseUrl = "https://localhost:5002/";
  const [errors, setErrors] = useState(null);

  function handleNotFound() {
    axios
      .get(baseUrl + "Error/not-found")
      .catch((err) => console.log(err.response));
  }

  function handleBadRequest() {
    axios
      .get(baseUrl + "Error/bad-request")
      .catch((err) => console.log(err.response));
  }

  function handleServerError() {
    axios
      .get(baseUrl + "Error/server-error")
      .catch((err) => console.log(err.response));
  }

  function handleUnauthorised() {
    axios
      .get(baseUrl + "Error/unauthorised")
      .catch((err) => console.log(err.response));
  }

  function HandleBadId() {
    axios.get(baseUrl + "Products/notaid").catch((err) => console.log(err));
  }

  function handleValidationError() {
    axios.post(baseUrl + "Products", {}).catch((err) => setErrors(err));
  }

  return (
    <>
      <Header as="h1" content="Test Error component" />
      <Segment>
        <Button.Group widths="7">
          <Button onClick={handleNotFound} content="Not Found" basic primary />
          <Button
            onClick={handleBadRequest}
            content="Bad Request"
            basic
            primary
          />
          <Button
            onClick={handleValidationError}
            content="Validation Error"
            basic
            primary
          />
          <Button
            onClick={handleServerError}
            content="Server Error"
            basic
            primary
          />
          <Button
            onClick={handleUnauthorised}
            content="Unauthorised"
            basic
            primary
          />
          <Button onClick={HandleBadId} content="Bad Id" basic primary />
        </Button.Group>
      </Segment>
      {errors && <ValidationErrors errors={errors} />}
    </>
  );
}
