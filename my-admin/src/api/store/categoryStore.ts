import { makeAutoObservable } from "mobx";
import consumer from "../consumer";
import { Category } from "../models/category";
import { selectOption } from "../models/option";

export default class CategoryStore{
    categories: Category[] = [];
    Coption: selectOption[] = [];

    constructor(){
        makeAutoObservable(this)
    }

    loadCategories = async () => {
        try {
            const categories = await consumer.Categories.list();
            categories.forEach(category => {
                this.setBrand(category);
                this.setOption(category);
            })
        } catch (error) {
            console.log(error);

        }
    }

    setBrand = (category: Category) => {
        this.categories = [...this.categories, category]
    }

    setOption = (category: Category) => {
        this.Coption = [...this.Coption, {text: category.name, value: category.name}]
    }
}