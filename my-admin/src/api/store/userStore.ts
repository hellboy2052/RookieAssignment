import { makeAutoObservable, runInAction } from "mobx";
import { toast } from "react-toastify";
import { history } from "../..";
import consumer from "../consumer";
import { User, UserFormValues } from "../models/user";
import { store } from "./store";


export default class UserStore {
    user: User | null = null;

    constructor() {
        makeAutoObservable(this);
    }


    get isLoggedIn() {
        return !!this.user;
    }

    login = async (form: UserFormValues) => {
        try {
            const user = await consumer.Account.login(form);
            // Check user Roles
            user.roles.forEach(role => {
                if (role != "customer") {
                    store.commonStore.setToken(user.token);
                    runInAction(() => this.user = user)
                    history.push("/dashboard");
                }
                toast.error("Customer are not allowed");
            });
            history.push("/");


        } catch (error) {
            throw (error);
        }
    }

    logout = () => {
        store.commonStore.setToken(null);
        window.localStorage.removeItem("jwt");
        this.user = null;
        history.push("/");
    }

    // get current login user
    getUser = async () => {
        try {
            const user = await consumer.Account.current();
            runInAction(() => this.user = user);
        } catch (error) {
            console.log(error);

        }
    }

}