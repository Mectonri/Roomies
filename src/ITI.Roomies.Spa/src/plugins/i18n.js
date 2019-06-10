import Vue from 'vue';
import VueI18n from 'vue-i18n';

Vue.use(VueI18n);

const messages = {
    'fr': {
        //A
        all: 'Tous',
        //B
        Bday: 'Date de naissance',
        //C
        colloc: 'Collocation',
        collocCreate: ' Créer une collocation puis inviter vos Roomies :',
        collocInvite: 'Rejoigner une collocation à partir d\'un code reçu par email :',
        collocInvite2: 'Inviter de nouveaux Roomies à votre collocation :',
        create: 'Création',
        //D
        description: 'Description',
        //E
        erreur: 'ERREUR',
        //F
        //G
        greeting: "Bienvenue Roomie",
        //H
        //I
        //J
        join :'Rejoindre',
        //K
        //L
        lang: 'Change to English',
        //M
        mouth: 'mois',
        modifier: 'Modifier',
        //N
        nullDesc: "Vous n'avez pas de description",
        number: 'Téléphone',
        Nom: 'Nom',
        //O
        //p
        pic: 'Photo :',
        Prenom: 'Prénom',
        Profil: 'Votre Profil',
        //Q
        //R
        //S
        //T
        testMsg: 'Bienvenue sur votre application Vus.js',
        //U
        //V
        //WXYZ
        week: "Heddomadaire",
        Welcome: 'Bienvenue sur',
        year: "Annuelle",
    },

    'en': {
        //A\\
        all: "All",

        //B
        Bday: 'Birthday',

        //C
        colloc: 'Flatsharing',
        collocCreate: 'Create a flatsharing to invite your Roomies',
        collocInvite: 'Join a flatsharing using the code revieved by email : ',
        collocInvite2: 'Invite new Roomies :',
        create: 'Create',

        //D
        day: 'Day',
        description: 'About you',

        //E
        erreur: 'ERROR',

        //G
        greeting: "Welcome Rommie",
//J
join :'Join',
        //L
        lang: 'Changer de langue',

        //M
        modifier: 'Modifier',
        mouth: 'mouth',
        number: 'Phone number',

        //N
        nullDesc: "You don't have a description",
        Nom: 'Name',

        //P
        Prenom: 'Firstname',
        Profil: 'Your Profile',
        pic: 'Profile picture',

        //T
        testMsg: 'Welcome to your Vue.js app',

        //WXYZ
        Welcome: 'Welcome to',
        week: 'Week',
        year: 'year',

    }
};

const i18n = new VueI18n({
    locale: 'fr',
    fallbackLocale: 'en',
    messages,
});
export default i18n;