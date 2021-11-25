import React from "react";

import { Page } from "@components/page";
import { Input } from "@components/input";
import { SimpleButton } from "@components/buttons/simple-button";

import styles from "./styles.module.scss";

export const Login: React.FC = () => {
  const form = React.createRef<HTMLFormElement>();

  function onSubmit(e: React.FormEvent<HTMLFormElement>) {
    e.preventDefault();
  }

  return (
    <Page>
      <div className={styles.topic}>
        <h1>Sign in</h1>
      </div>
      <form className={styles.formContent} onSubmit={onSubmit} ref={form}>
        <div className={styles.inputs}>
          <Input text={"Email"} type={"email"} required />
          <Input text={"Password"} type={"password"} required />
        </div>
        <SimpleButton text={"Sign in"} size={"medium"} type={"submit"} />
      </form>
      <div className={styles.signup}>
        <h3>Not signed up?</h3>
        <SimpleButton text={"Sign up"} size={"small"} />
      </div>
    </Page>
  );
};
