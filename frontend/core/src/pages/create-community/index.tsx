import React, { useState } from "react";
import { useHistory } from "react-router-dom";

import { CommunitiesService } from "@api/services/communities.service";

import { Page } from "@components/page";
import { SimpleButton } from "@components/buttons/simple-button";
import { Textarea } from "@components/textarea";
import { Input } from "@components/input";

import styles from "./styles.module.scss";

export const CreateCommunity: React.FC = () => {
  const [name, setName] = useState("");
  const [description, setDescription] = useState("");
  const history = useHistory();

  function onSubmit(e: React.FormEvent<HTMLFormElement>) {
    e.preventDefault();
    CommunitiesService.createCommunity(name, description).then(communityId => {
      history.push("/communities/" + communityId);
    });
  }

  return (
    <Page>
      <div className={styles.topic}>
        <h1>Create community</h1>
      </div>
      <form className={styles.form} onSubmit={onSubmit}>
        <Input
          text={""}
          placeholder={"Name"}
          onChange={(e: React.ChangeEvent<HTMLInputElement>) => {
            setName(e.target.value);
          }}
          required
        />
        <Textarea
          onChange={(e: React.ChangeEvent<HTMLTextAreaElement>) => {
            setDescription(e.target.value);
          }}
          placeholder={"Description"}
          required
        />
        <SimpleButton text={"Post"} size={"medium"} type={"submit"} />
      </form>
    </Page>
  );
};
