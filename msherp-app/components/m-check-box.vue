<template>
	<view class="cu-form-group margin-top">
		<checkbox :checked="ischecked" @change="onChange" color="#FFCC33" style="transform:scale(0.7)" />记住我
	</view>
</template>

<script>
	export default {
		data() {
			return {
				ischecked: true
			}
		},
		model: {
			prop: 'checked',
			event: 'change'
		},
		methods: {
			onChange(e) {
				this.$emit('change', e.target.checked)
			}
		},
		props: {
			checked: {
				type: Boolean,
				default: false
			},
			disabled: {
				type: Boolean,
				default: false
			}
		}
	}
</script>
