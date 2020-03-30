import React, { useState, useEffect } from "react";
import styles from "./Account.module.css";
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import AccountData from "../../components/AccountData/AccountData";
import AccountButtons from "../../components/AccountButtons/AccountButtons";
import axios from "axios";

const Account = props => {
  const apiUrl = "https://localhost:44392/api";

  const [accountData, setAccountData] = useState({
    balance: null,
    status: null
  });
  const [wasMoneyDeposited, setWasMoneyDeposited] = useState(false);
  const [wasMoneyWithdrawed, setWasMoneyWithdrawed] = useState(false);
  const [isServerAvailable, setIsServerAvailable] = useState(true);

  useEffect(() => getBalanceAndStatusHandler(), []);

  useEffect(() => {
    if (wasMoneyDeposited || wasMoneyWithdrawed) {
      getBalanceAndStatusHandler();
    }
  }, [wasMoneyDeposited, wasMoneyWithdrawed]);

  toast.configure({
    autoClose: 3000,
    draggable: false
  });

  const handleResponseError = error => {
    let errorMessage = "Server unavailable!";
    if (error.response) {
      errorMessage = error.response.data
        ? error.response.data
        : error.response.statusText;
    } else {
      setIsServerAvailable(false);
    }
    toast.error(errorMessage, {
      position: toast.POSITION.BOTTOM_RIGHT
    });
  };

  const getBalanceAndStatusHandler = () => {
    setIsServerAvailable(true);
    axios
      .get(`${apiUrl}/Queries/Account/GetBalanceAndStatus`)
      .then(response => {
        setAccountData({
          balance: response.data.balance.toFixed(2),
          status: response.data.status
        });
      })
      .catch(error => handleResponseError(error));
  };

  const depositMoneyHandler = data => {
    setIsServerAvailable(true);
    setWasMoneyDeposited(false);
    axios
      .post(`${apiUrl}/Commands/Account/DepositMoney`, data)
      .then(response => {
        if (response.status === 200) {
          setWasMoneyDeposited(true);
        }
      })
      .catch(error => handleResponseError(error));
  };

  const withdrawMoneyHandler = data => {
    setIsServerAvailable(true);
    setWasMoneyWithdrawed(false);
    axios
      .post(`${apiUrl}/Commands/Account/WithdrawMoney`, data)
      .then(response => {
        if (response.status === 200) {
          setWasMoneyWithdrawed(true);
        }
      })
      .catch(error => handleResponseError(error));
  };

  return (
    <div className={styles.Account}>
      <AccountData
        balance={accountData.balance}
        status={accountData.status}
        isServerAvailable={isServerAvailable}
      />
      <AccountButtons
        depositMoneyClicked={depositMoneyHandler}
        withdrawMoneyClicked={withdrawMoneyHandler}
      />
    </div>
  );
};

export default Account;
