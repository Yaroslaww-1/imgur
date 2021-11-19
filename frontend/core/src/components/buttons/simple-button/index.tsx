import React, { MouseEvent } from "react";

import styles from "./styles.module.scss";

interface ISimpleButton {
  text: string;
  size: string;
  [prop: string]: unknown;
}

export const SimpleButton: React.FC<ISimpleButton> = props => {
  return (
    <button
      className={
        props.size === "small" ? `${styles.smallBtn} ${styles.btn}`
        // eslint-disable-next-line indent
        :props.size === "medium" ? `${styles.mediumBtn} ${styles.btn}`
          // eslint-disable-next-line indent
        :`${styles.largeBtn} ${styles.btn}`
      }
      {...props}
    >
      <span>{props.text}</span>
    </button>
  );
};
