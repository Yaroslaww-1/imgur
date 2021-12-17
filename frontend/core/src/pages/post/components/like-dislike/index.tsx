import React, { useEffect, useState } from "react";
import { observer } from "mobx-react-lite";

import styles from "./styles.module.scss";

interface IProps {
  likes: number;
  dislikes: number;
  isLiked: boolean;
  isDisliked: boolean;
  onLike: () => void;
  onDislike: () => void;
}

export const LikeDislikeComponent: React.FC<IProps> = observer(props => {
  const [likes, setLike] = useState(0);
  const [dislikes, setDislike] = useState(0);
  const [isLiked, setIsLiked] = useState(false);
  const [isDisliked, setIsDisliked] = useState(false);

  useEffect(() => {
    setLike(props.likes);
    setDislike(props.dislikes);
    setIsLiked(props.isLiked);
    setIsDisliked(props.isDisliked);
  }, [props]);

  function onLikeClick() {
    if (!isLiked && !isDisliked) {
      setLike(likes + 1);
      setIsLiked(true);
      props.onLike();
    } else if (!isLiked && isDisliked) {
      setLike(likes + 1);
      setDislike(dislikes - 1);
      setIsLiked(true);
      setIsDisliked(false);
      props.onLike();
    } else if (isLiked) {
      setLike(likes - 1);
      setIsLiked(false);
      props.onLike();
    }
  }

  function onDislikeClick() {
    if (!isDisliked && !isLiked) {
      setDislike(dislikes + 1);
      setIsDisliked(true);
      props.onDislike();
    } else if (!isDisliked && isLiked) {
      setLike(likes - 1);
      setDislike(dislikes + 1);
      setIsLiked(false);
      setIsDisliked(true);
      props.onDislike();
    } else if (isDisliked) {
      setDislike(dislikes - 1);
      setIsDisliked(false);
      props.onDislike();
    }
  }

  return (
    <div className={styles.contentWrapper}>
      <div className={styles.reaction}>
        <i
          className={isLiked ? "fa fa-thumbs-up" : "fa fa-thumbs-o-up"}
          onClick={onLikeClick}
        />
        {likes}
      </div>
      <div className={styles.reaction}>
        <i
          className={isDisliked ? "fa fa-thumbs-down" : "fa fa-thumbs-o-down"}
          onClick={onDislikeClick}
        />
        {dislikes}
      </div>
    </div>
  );
});
