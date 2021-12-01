import React, { useContext, useState } from "react";
import { useHistory } from "react-router-dom";
import { observer } from "mobx-react-lite";

import { Context } from "index";
import { AppRoute } from "@common/enums/app-route.enum";

import { Page } from "@components/page";
import { Input } from "@components/input";
import { SimpleButton } from "@components/buttons/simple-button";

import styles from "./styles.module.scss";

export const Signup: React.FC = observer(() => {
  const { store } = useContext(Context);
  const history = useHistory();
  const [email, setEmail] = useState("");
  const [name, setName] = useState("");
  const [password, setPassword] = useState("");

  function onSubmit(e: React.FormEvent<HTMLFormElement>) {
    e.preventDefault();
    store.registration(email, name, password);
    history.push(AppRoute.LOGIN);
  }

  return (
    <Page>
      <div className={styles.topic}>
        <h1>Sign up</h1>
      </div>
      <form className={styles.formContent} onSubmit={onSubmit}>
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
            text={"Name"}
            type={"text"}
            required
            onChange={(e: React.ChangeEvent<HTMLInputElement>) => {
              setName(e.target.value);
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
        <SimpleButton text={"Sign up"} size={"medium"} type={"submit"} />
      </form>
    </Page>
  );
});
