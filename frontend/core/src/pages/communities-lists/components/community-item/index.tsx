import React from "react";
import { useHistory } from "react-router-dom";

import { ICommunity } from "@models/community.model";

import styles from "./styles.module.scss";
import { SimpleButton } from "@components/buttons/simple-button";

interface IProps {
  community: ICommunity;
  subscribed: boolean;
  handleJoinClick: (community: ICommunity) => void;
  handleLeaveClick: (community: ICommunity) => void;
}

export const CommunityItem: React.FC<IProps> = props => {
  const history = useHistory();

  function redirectToCommunity() {
    history.push("/communities/" + props.community.id);
  }

  return (
    <div className={styles.item}>
      <div>
        <div className={styles.name} onClick={redirectToCommunity}>
          {props.community.name}
        </div>
        <div className={styles.description}>{props.community.description}</div>
      </div>
      <div className={styles.subscribe}>
        {props.subscribed ? (
          <SimpleButton
            text={"Leave"}
            size={"small"}
            onClick={() => {
              props.handleLeaveClick(props.community);
            }}
          />
        ) : (
          <SimpleButton
            text={"Join"}
            size={"small"}
            onClick={() => {
              props.handleJoinClick(props.community);
            }}
          />
        )}
      </div>
    </div>
  );
};
