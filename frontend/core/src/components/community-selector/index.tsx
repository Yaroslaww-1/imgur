import React from "react";

import { ICommunity } from "@models/community.model";

import styles from "./styles.module.scss";

interface IProps {
  array: ICommunity[];
  onSelect: (e: React.ChangeEvent<HTMLSelectElement>) => void;
}

export const CommunitySelect: React.FC<IProps> = ({ array, onSelect }) => {
  return (
    <section className={styles.container}>
      {array.length != 0 ? (
        <div className={styles.dropdown}>
          Community:
          <select className={styles.dropdownSelect} onChange={onSelect}>
            {array.map(community => (
              <option key={community.id} value={community.id}>
                {community.name}
              </option>
            ))}
          </select>
        </div>
      ) : (
        "You have no communities("
      )}
    </section>
  );
};
