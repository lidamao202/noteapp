
import { defineStore } from 'pinia'

export const userStore = defineStore('user', {

    state: () => ({
        id: "", 
        userName:"" ,
        token:""
    }),
    actions:{
      setId(id:string){
        this.id=id;
      },
      setUserName(userName:string){
        this.userName=userName;
      }
    },
    getters:{
      getId:(state) => state.id
    }
  })

// export const userStore = defineStore('counter', () => {
//   const count = ref(0);
//   const id=ref('');
//   const userName=ref('');

//   const doubleCount = computed(() => count.value * 2)
//   function increment() {
//     count.value++
//   }

//   return { count, doubleCount, increment,id,userName }
// })
