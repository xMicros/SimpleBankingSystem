import React from "react";
import styles from "./AccountButtons.module.css";

const AccountButtons = props => {
  const fixedMoneyAmount = { MoneyAmount: 50 };

  return (
    <div className={styles.AccountButtons}>
      <button onClick={() => props.depositMoneyClicked(fixedMoneyAmount)}>
        Deposit money ({fixedMoneyAmount.MoneyAmount})
      </button>
      <button onClick={() => props.withdrawMoneyClicked(fixedMoneyAmount)}>
        Withdraw money ({fixedMoneyAmount.MoneyAmount})
      </button>
    </div>
  );
};

export default AccountButtons;
