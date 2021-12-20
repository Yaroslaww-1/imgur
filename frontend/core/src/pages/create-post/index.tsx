import React, { useEffect, useState } from "react";
import { useHistory } from "react-router-dom";

import { PostsService } from "@api/services/posts.service";
import { CommunitiesService } from "@api/services/communities.service";

import { ICommunity } from "@models/community.model";

import { Page } from "@components/page";
import { SimpleButton } from "@components/buttons/simple-button";
import { ImageUploader } from "@components/image-uploader";
import { Textarea } from "@components/textarea";
import { Input } from "@components/input";
import { CommunitySelect } from "@components/community-selector";

import styles from "./styles.module.scss";

export const CreatePost: React.FC = () => {
  const [name, setName] = useState("");
  const [content, setContent] = useState("");
  const [image, setImage] = useState("");
  const [communityId, setCommunityId] = useState("");
  const [communities, setCommunities] = useState<ICommunity[]>();
  const history = useHistory();

  useEffect(() => {
    CommunitiesService.getUserCommunities().then(communities => {
      setCommunities(communities);
      if (communities && communities?.length != 0) {
        setCommunityId(communities[0].id);
      }
    });
  }, []);

  function onSubmit(e: React.FormEvent<HTMLFormElement>) {
    e.preventDefault();
    if ((image === "" && content === "") || communityId === "") return;
    PostsService.createPost(communityId, name, content, image).then(postId => {
      history.push("/posts/" + postId);
    });
  }

  function handleFileUpload(newImage: File) {
    const reader = new FileReader();
    reader.readAsDataURL(newImage);
    reader.onload = () => {
      if (typeof reader.result === "string")
        setImage(reader.result.split(",")[1]);
    };
  }

  function onSelect(e: React.ChangeEvent<HTMLSelectElement>) {
    setCommunityId(e.target.value);
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
          <CommunitySelect array={communities || []} onSelect={onSelect} />
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
