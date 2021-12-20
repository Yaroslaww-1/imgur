import React, { useState } from "react";

import { PostsService } from "@api/services/posts.service";

import { Page } from "@components/page";
import { SimpleButton } from "@components/buttons/simple-button";
import { ImageUploader } from "@components/image-uploader";
import { Textarea } from "@components/textarea";
import { Input } from "@components/input";

import styles from "./styles.module.scss";

export const CreatePost: React.FC = () => {
  const [name, setName] = useState("");
  const [content, setContent] = useState("");
  const [image, setImage] = useState("");

  function onSubmit(e: React.FormEvent<HTMLFormElement>) {
    e.preventDefault();
    // TODO: add community selection
    // PostsService.createPost(communityId, name, content, image);
    PostsService.createPost(
      "11111111-1111-1111-1111-111111111111",
      name,
      content,
      image,
    );
  }

  function handleFileUpload(newImage: File) {
    const reader = new FileReader();
    reader.readAsDataURL(newImage);
    reader.onload = () => {
      if (typeof reader.result === "string")
        setImage(reader.result.split(",")[1]);
    };
  }

  return (
    <Page>
      <div className={styles.topic}>
        <h1>Create post</h1>
      </div>
      <form className={styles.form} onSubmit={onSubmit}>
        <div className={styles.imageUploaderWrapper}>
          <ImageUploader onUpload={handleFileUpload} />
        </div>
        <div className={styles.contentWrapper}>
          <Input
            text={""}
            placeholder={"Name"}
            onChange={(e: React.ChangeEvent<HTMLInputElement>) => {
              setName(e.target.value);
            }}
          />
          <Textarea
            onChange={(e: React.ChangeEvent<HTMLTextAreaElement>) => {
              setContent(e.target.value);
            }}
            placeholder={"Content"}
          />
          <SimpleButton text={"Post"} size={"medium"} type={"submit"} />
        </div>
      </form>
    </Page>
  );
};
