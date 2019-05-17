<template>
    <section>
        <div>
            <h5>{{$t('lang')}}</h5>
            <el-button @click="changeLocale('fr')">Fr</el-button>
            <el-button @click="changeLocale('en')">En</el-button>
        </div>
    </section>
</template>

<script>

import i18n from '../plugins/i18n';

export default {

    methods: {
            changeLocale(locale) {
                   i18n.locale = locale;
            }
    }
}

</script>