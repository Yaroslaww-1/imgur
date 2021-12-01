import React, { useContext, useState } from "react";

import { Context } from "index";

import { Page } from "@components/page";
import { Input } from "@components/input";
import { SimpleButton } from "@components/buttons/simple-button";

import styles from "./styles.module.scss";
import { observer } from "mobx-react-lite";

export const Login: React.FC = observer(() => {
  const { store } = useContext(Context);
  const form = React.createRef<HTMLFormElement>();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  function onSubmit(e: React.FormEvent<HTMLFormElement>) {
    e.preventDefault();
    store.login(email, password);
  }

  return (
    <Page>
      <div className={styles.topic}>
        <h1>Sign in</h1>
      </div>
      <form className={styles.formContent} onSubmit={onSubmit} ref={form}>
        <div className={styles.inputs}>
          <Input
            text={"Email"}
            type={"email"}
            required
            onChange={(e: React.ChangeEvent<HTMLInputElement>) => {
              setEmail(e.target.value);
            }}
          />
          <Input
            text={"Password"}
            type={"password"}
            required
            onChange={(e: React.ChangeEvent<HTMLInputElement>) => {
              setPassword(e.target.value);
            }}
          />
        </div>
        <SimpleButton text={"Sign in"} size={"medium"} type={"submit"} />
      </form>
      <div className={styles.signup}>
        <h3>Not signed up?</h3>
        <SimpleButton text={"Sign up"} size={"small"} />
      </div>

      <SimpleButton
        text={"Log out"}
        size={"small"}
        onClick={(e: React.MouseEvent<HTMLButtonElement>) => {
          store.logout();
        }}
      />
    </Page>
  );
});
