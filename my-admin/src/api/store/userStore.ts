import { makeAutoObservable, runInAction } from "mobx";
import { toast } from "react-toastify";
import { StringLiteralLike } from "typescript";
import { history } from "../..";
import consumer from "../consumer";
import { User, UserFormValues, UserData } from "../models/user";
import { store } from "./store";


export default class UserStore {
    user: User | null = null;
    userRegistry = new Map<string, UserData>();
    loadingInitial = false;

    constructor() {
        makeAutoObservable(this);
    }

    get UserByUsername() {
        return Array.from(this.userRegistry.values()).sort((a, b) =>
            a.username.localeCompare(b.username))
    }


    get isLoggedIn() {
        return !!this.user;
    }

    loadUser = async () => {
        this.setLoadingInitial(true)

        try {
            const users = await consumer.Account.list();
            users.forEach(user => {
                this.setUser(user);
            })
            this.setLoadingInitial(false);
        } catch (error) {
            console.log(error);
            this.setLoadingInitial(false);

        }
    }

    setUser = (user: UserData) => {
        this.userRegistry.set(user.username, user);
    }

    login = async (form: UserFormValues) => {
        try {
            const user = await consumer.Account.login(form);
            // Check user Roles
            user.roles.forEach(role => {
                if (role !== "customer") {
                    store.commonStore.setToken(user.token);
                    runInAction(() => this.user = user)
                    history.push("/dashboard");
                } else {
                    toast.error("Customer are not allowed");
                }

            });



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

    setLoadingInitial = (state: boolean) => {
        this.loadingInitial = state;
    }

}